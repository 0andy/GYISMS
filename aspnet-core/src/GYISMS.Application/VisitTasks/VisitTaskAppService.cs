
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;

using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

using GYISMS.VisitTasks.Authorization;
using GYISMS.VisitTasks.Dtos;
using GYISMS.VisitTasks;
using GYISMS.Authorization;
using GYISMS.TaskExamines;
using GYISMS.TaskExamines.Dtos;

namespace GYISMS.VisitTasks
{
    /// <summary>
    /// VisitTask应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize(AppPermissions.Pages)]
    public class VisitTaskAppService : GYISMSAppServiceBase, IVisitTaskAppService
    {
        private readonly IRepository<VisitTask, int> _visittaskRepository;
        private readonly IVisitTaskManager _visittaskManager;
        private readonly IRepository<TaskExamine, int> _taskexamineRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public VisitTaskAppService(IRepository<VisitTask, int> visittaskRepository
            , IVisitTaskManager visittaskManager
            , IRepository<TaskExamine, int> taskexamineRepository
            )
        {
            _visittaskRepository = visittaskRepository;
            _visittaskManager = visittaskManager;
            _taskexamineRepository = taskexamineRepository;
        }


        /// <summary>
        /// 获取VisitTask的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<VisitTaskListDto>> GetPagedVisitTasksAsync(GetVisitTasksInput input)
        {
            var query = _visittaskRepository.GetAll().Where(v => v.IsDeleted == false)
                     .WhereIf(!string.IsNullOrEmpty(input.Name), u => u.Name.Contains(input.Name));

            // TODO:根据传入的参数添加过滤条件

            var visittaskCount = await query.CountAsync();

            var visittasks = await query
                    .OrderBy(v => v.Type).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var visittaskListDtos = ObjectMapper.Map<List <VisitTaskListDto>>(visittasks);
            var visittaskListDtos = visittasks.MapTo<List<VisitTaskListDto>>();

            return new PagedResultDto<VisitTaskListDto>(
                    visittaskCount,
                    visittaskListDtos
                );
        }

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetVisitTaskForEditOutput> GetVisitTaskForEdit(NullableIdDto<int> input)
        {
            var output = new GetVisitTaskForEditOutput();
            VisitTaskEditDto visittaskEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _visittaskRepository.GetAsync(input.Id.Value);

                visittaskEditDto = entity.MapTo<VisitTaskEditDto>();

                //visittaskEditDto = ObjectMapper.Map<List <visittaskEditDto>>(entity);
            }
            else
            {
                visittaskEditDto = new VisitTaskEditDto();
            }

            output.VisitTask = visittaskEditDto;
            return output;
        }


        /// <summary>
        /// 新增VisitTask
        /// </summary>
        protected virtual async Task<VisitTaskListDto> CreateVisitTaskAsync(VisitTaskEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = ObjectMapper.Map<VisitTask>(input);
            entity.IsDeleted = false;
            var id = await _visittaskRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            foreach (var item in input.TaskExamineList)
            {
                item.TaskId = id;
                var examEntity = ObjectMapper.Map<TaskExamine>(item);
                await _taskexamineRepository.InsertAndGetIdAsync(examEntity);
            }
            return entity.MapTo<VisitTaskListDto>();
        }

        /// <summary>
        /// 编辑VisitTask
        /// </summary>
        protected virtual async Task<VisitTaskListDto> UpdateVisitTaskAsync(VisitTaskEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _visittaskRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);
            // ObjectMapper.Map(input, entity);
            var result = await _visittaskRepository.UpdateAsync(entity);
            if (entity.IsExamine ==true)
            {
                foreach (var item in input.TaskExamineList)
                {
                    if (item.Id == 0)
                    {
                        await UpdateTaskExamineAsync(item);
                    }
                    else
                    {
                        await CreateTaskExamineAsync(item);
                    }
                }
                
                //var examEntity = await _taskexamineRepository.GetAll().Where(v => v.TaskId == entity.Id).ToListAsync();
                //foreach (var item in examEntity)
                //{
                //    var xentity = await _visittaskRepository.GetAsync(item.Id);
                //    input.MapTo(entity);
                //    // ObjectMapper.Map(input, entity);
                //    var xresult = await _visittaskRepository.UpdateAsync(entity);
                //}
            }
            return result.MapTo<VisitTaskListDto>();
        }

        /// <summary>
        /// 添加或者修改TaskExamine的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task CreateOrUpdateTaskExamineAsync(TaskExamineEditDto input)
        {

            if (input.Id == 0)
            {
                await UpdateTaskExamineAsync(input);
            }
            else
            {
                await CreateTaskExamineAsync(input);
            }
        }

        /// <summary>
        /// 新增TaskExamine
        /// </summary>
        protected virtual async Task CreateTaskExamineAsync(TaskExamineEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = ObjectMapper.Map<TaskExamine>(input);
            entity.IsDeleted = false;
            await _taskexamineRepository.InsertAndGetIdAsync(entity);
        }

        /// <summary>
        /// 编辑TaskExamine
        /// </summary>
        protected virtual async Task UpdateTaskExamineAsync(TaskExamineEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _taskexamineRepository.GetAsync(input.Id);
            input.MapTo(entity);
            await _taskexamineRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除VisitTask信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(VisitTaskAppPermissions.VisitTask_Delete)]
        public async Task DeleteVisitTask(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _visittaskRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除VisitTask的方法
        /// </summary>
        [AbpAuthorize(VisitTaskAppPermissions.VisitTask_BatchDelete)]
        public async Task BatchDeleteVisitTasksAsync(List<int> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _visittaskRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 新增或修改任务信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<VisitTaskListDto> CreateOrUpdateVisitTaskAsycn(VisitTaskEditDto input)
        {
            if (input.Id.HasValue)
            {
                return await UpdateVisitTaskAsync(input);
            }
            else
            {
                return await CreateVisitTaskAsync(input);
            }
        }

        /// <summary>
        /// 根据id获取任务信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VisitTaskListDto> GetVisitTaskByIdAsync(int id)
        {
            var entity = await _visittaskRepository.GetAsync(id);
            return entity.MapTo<VisitTaskListDto>();
        }

        /// <summary>
        /// 删除任务信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task VisitTaskDeleteByIdAsync(VisitTaskEditDto input)
        {
            var entity = await _visittaskRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);
            entity.IsDeleted = true;
            entity.DeletionTime = DateTime.Now;
            entity.DeleterUserId = AbpSession.UserId;
            await _visittaskRepository.UpdateAsync(entity);
        }
    }
}