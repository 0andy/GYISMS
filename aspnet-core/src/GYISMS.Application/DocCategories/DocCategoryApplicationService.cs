
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using GYISMS.DocCategories;
using GYISMS.DocCategories.Dtos;
using GYISMS.DocCategories.DomainService;
using GYISMS.Documents.Dtos;
using GYISMS.Documents;
using GYISMS.Employees;

namespace GYISMS.DocCategories
{
    /// <summary>
    /// DocCategory应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class DocCategoryAppService : GYISMSAppServiceBase, IDocCategoryAppService
    {
        private readonly IRepository<DocCategory, int> _entityRepository;
        private readonly IRepository<Document, Guid> _documentRepository;

        private readonly IDocCategoryManager _entityManager;
        private readonly IRepository<Employee, string> _employeeRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public DocCategoryAppService(
        IRepository<DocCategory, int> entityRepository
        , IDocCategoryManager entityManager
            , IRepository<Document, Guid> documentRepository
            , IRepository<Employee, string> employeeRepository

        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
            _documentRepository = documentRepository;
            _employeeRepository = employeeRepository;
        }


        /// <summary>
        /// 获取DocCategory的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<DocCategoryListDto>> GetPaged(GetDocCategorysInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<DocCategoryListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<DocCategoryListDto>>();

            return new PagedResultDto<DocCategoryListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取DocCategoryListDto信息
        /// </summary>

        public async Task<DocCategoryListDto> GetById(EntityDto<int> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<DocCategoryListDto>();
        }

        /// <summary>
        /// 获取编辑 DocCategory
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetDocCategoryForEditOutput> GetForEdit(NullableIdDto<int> input)
        {
            var output = new GetDocCategoryForEditOutput();
            DocCategoryEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<DocCategoryEditDto>();

                //docCategoryEditDto = ObjectMapper.Map<List<docCategoryEditDto>>(entity);
            }
            else
            {
                editDto = new DocCategoryEditDto();
            }

            output.DocCategory = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改DocCategory的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateDocCategoryInput input)
        {
            input.DocCategory.ParentId = input.DocCategory.ParentId ?? 0;
            if (input.DocCategory.Id != 0)
            {
                await Update(input.DocCategory);
            }
            else
            {
                await Create(input.DocCategory);
            }
        }


        /// <summary>
        /// 新增DocCategory
        /// </summary>

        protected virtual async Task<DocCategoryEditDto> Create(DocCategoryEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <DocCategory>(input);
            var entity = input.MapTo<DocCategory>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<DocCategoryEditDto>();
        }

        /// <summary>
        /// 编辑DocCategory
        /// </summary>

        protected virtual async Task Update(DocCategoryEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除DocCategory信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除DocCategory的方法
        /// </summary>

        public async Task BatchDelete(List<int> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        private List<CategoryTreeNode> GetTrees(int pid, List<DocCategory> categoryList)
        {
            var catQuery = categoryList.Where(c => c.ParentId == pid)
                            .Select(c => new CategoryTreeNode()
                            {
                                key = c.Id.ToString(),
                                title = c.Name,
                                ParentId = c.ParentId,
                                children = GetTrees(c.Id, categoryList)
                            });
            return catQuery.ToList();
        }

        public async Task<List<CategoryTreeNode>> GetTreeAsync()
        {
            var categoryList = await _entityRepository.GetAllListAsync();
            return GetTrees(0, categoryList);
        }

        /// <summary>
        /// 钉钉获取知识库类别
        /// </summary>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<List<GridListDto>> GetCategoryListAsync(string host, string userId)
        {
            var user = await _employeeRepository.GetAll().Where(v => v.Id == userId).FirstOrDefaultAsync();
            var departmentId = user.Department.Replace('[', ' ').Replace(']', ' ').Trim();
            var query = _entityRepository.GetAll().Where(v => v.ParentId == 0).OrderBy(v => v.Id).AsNoTracking();
            var entityList = from c in query
                             select new GridListDto()
                             {
                                 Id = c.Id,
                                 Text = c.Name,
                                 Icon = host + "knowledge/homePage.png"
                             };
            List<GridListDto> list = new List<GridListDto>();
            foreach (var item in entityList)
            {
                int count = await _documentRepository.GetAll().Where(v => ("," + v.CategoryCode + ",").Contains("," + item.Id.ToString() + ",") && (v.IsAllUser == true || v.DeptIds.Contains(departmentId) || v.EmployeeIds.Contains(userId))).AsNoTracking().CountAsync();
                if (count > 0)
                {
                    list.Add(item);
                }
            }
            return list;
        }


        /// <summary>
        /// 钉钉获取Tab子列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<List<TabListDto>> GetTabChildListByIdAsync(int id)
        {
            var query = _entityRepository.GetAll().Where(v => v.ParentId == id).AsNoTracking();
            List<TabListDto> list = new List<TabListDto>();
            TabListDto item = new TabListDto();
            item.Id = id;
            item.ParentId = 0;
            item.Title = "全部";
            list.Add(item);
            var result = await (from t in query
                                select new TabListDto()
                                {
                                    Id = t.Id,
                                    ParentId = t.ParentId,
                                    Title = t.Name
                                }).AsNoTracking().ToListAsync();
            list.AddRange(result);
            return list;
        }
    }
}


