
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

using GYISMS.ScheduleTasks.Authorization;
using GYISMS.ScheduleTasks.Dtos;
using GYISMS.ScheduleTasks;
using GYISMS.Authorization;

namespace GYISMS.ScheduleTasks
{
    /// <summary>
    /// ScheduleTask应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize(AppPermissions.Pages)]
    public class ScheduleTaskAppService : GYISMSAppServiceBase, IScheduleTaskAppService
    {
        private readonly IRepository<ScheduleTask, Guid> _scheduletaskRepository;
        private readonly IScheduleTaskManager _scheduletaskManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ScheduleTaskAppService(IRepository<ScheduleTask, Guid> scheduletaskRepository
            , IScheduleTaskManager scheduletaskManager
            )
        {
            _scheduletaskRepository = scheduletaskRepository;
            _scheduletaskManager = scheduletaskManager;
        }


        /// <summary>
        /// 获取ScheduleTask的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ScheduleTaskListDto>> GetPagedScheduleTasksAsync(GetScheduleTasksInput input)
        {

            var query = _scheduletaskRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件

            var scheduletaskCount = await query.CountAsync();

            var scheduletasks = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var scheduletaskListDtos = ObjectMapper.Map<List <ScheduleTaskListDto>>(scheduletasks);
            var scheduletaskListDtos = scheduletasks.MapTo<List<ScheduleTaskListDto>>();

            return new PagedResultDto<ScheduleTaskListDto>(
                        scheduletaskCount,
                        scheduletaskListDtos
                );
        }


        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetScheduleTaskForEditOutput> GetScheduleTaskForEdit(NullableIdDto<Guid> input)
        {
            var output = new GetScheduleTaskForEditOutput();
            ScheduleTaskEditDto scheduletaskEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _scheduletaskRepository.GetAsync(input.Id.Value);

                scheduletaskEditDto = entity.MapTo<ScheduleTaskEditDto>();

                //scheduletaskEditDto = ObjectMapper.Map<List <scheduletaskEditDto>>(entity);
            }
            else
            {
                scheduletaskEditDto = new ScheduleTaskEditDto();
            }

            output.ScheduleTask = scheduletaskEditDto;
            return output;
        }


        /// <summary>
        /// 新增ScheduleTask
        /// </summary>
        protected virtual async Task<ScheduleTaskEditDto> CreateScheduleTaskAsync(ScheduleTaskEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增
            if (input.VisitNum == null)
            {
                input.VisitNum = 5;
            }
            var entity = ObjectMapper.Map<ScheduleTask>(input);
            entity.CreationTime = DateTime.Now;
            entity = await _scheduletaskRepository.InsertAsync(entity);
            return entity.MapTo<ScheduleTaskEditDto>();
        }

        /// <summary>
        /// 编辑ScheduleTask
        /// </summary>
        protected virtual async Task<ScheduleTaskEditDto> UpdateScheduleTaskAsync(ScheduleTaskEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _scheduletaskRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            var result = await _scheduletaskRepository.UpdateAsync(entity);
            return result.MapTo<ScheduleTaskEditDto>();
        }



        /// <summary>
        /// 删除ScheduleTask信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ScheduleTaskAppPermissions.ScheduleTask_Delete)]
        public async Task DeleteScheduleTask(EntityDto<Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _scheduletaskRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除ScheduleTask的方法
        /// </summary>
        [AbpAuthorize(ScheduleTaskAppPermissions.ScheduleTask_BatchDelete)]
        public async Task BatchDeleteScheduleTasksAsync(List<Guid> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _scheduletaskRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 新增或修改计划任务信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ScheduleTaskEditDto> CreateOrUpdateScheduleTaskAsycn(ScheduleTaskEditDto input)
        {
            if (input.Id.HasValue)
            {
                return await UpdateScheduleTaskAsync(input);
            }
            else
            {
                return await CreateScheduleTaskAsync(input);
            }
        }

        /// <summary>
        /// 根据id获取计划任务信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ScheduleTaskListDto> GetScheduleTaskByIdAsync(Guid id)
        {
            var entity = await _scheduletaskRepository.GetAsync(id);
            return entity.MapTo<ScheduleTaskListDto>();
        }
    }
}

