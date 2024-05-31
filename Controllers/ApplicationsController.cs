using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using WeddingSample.Filters;
using WeddingSample.Models;

namespace WeddingSample.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private ApplicationContext _applicationContext;

        public ApplicationsController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Application>>> GetAllAsync(string name)
        {
            var result = _applicationContext.Applications.Select(a => a);

            if (!string.IsNullOrWhiteSpace(name))
            {
                result = result.Where(x => x.Name.Contains(name));
            }
            await result.ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetOneAsync(Guid id)
        {
            var temp = _applicationContext.Applications.Where(x => x.PartitionKey == id);
            if (temp == null)
            {
                return NotFound("找不到當事人");
            }


            return Ok(await temp.SingleOrDefaultAsync());
        }

        //[CheckRefererFilter]
        [HttpPost]
        public async Task<ActionResult<Application>> Post(Application temp)
        {
            Application insesrt = new Application()
            {
                Name = temp.Name,
                Address = temp.Address,
                Phone = temp.Phone,
                Count_Person = temp.Count_Person,
                Count_Child = temp.Count_Child,
                Count_Veg = temp.Count_Veg,
                Message = temp.Message
            };
            _applicationContext.Applications.Add(insesrt);
            _applicationContext.SaveChanges();
            return Ok(insesrt);
        }


    }
}
//使用Azure雲端表格存取資料
//CloudTable applicationTable = DBConnection.AuthTable();

//[HttpGet]
//public async Task<ActionResult<List<Application>>> GetAllAsync()
//{
//    TableQuery<Application> query = new TableQuery<Application>();
//    List<Application> resultList = new List<Application>();


//    // Print the fields for each customer.
//    TableContinuationToken token = null;
//    do
//    {
//        TableQuerySegment<Application> resultSegment = await applicationTable.ExecuteQuerySegmentedAsync(query, token);
//        token = resultSegment.ContinuationToken;

//        foreach (Application entity in resultSegment.Results)
//        {
//            resultList.Add(entity);
//        }
//    } while (token != null);

//    return resultList;
//}

//[HttpGet("{UUID}", Name = "Applications")]
//public async Task<ActionResult<Application>> GetByUUIDAsync(string UUID)
//{

//    TableOperation retrieveOperation = TableOperation.Retrieve<Application>(UUID, "");

//    // Execute the retrieve operation.
//    TableResult retrievedResult = await applicationTable.ExecuteAsync(retrieveOperation);

//    if (retrievedResult.Result is null)
//    {
//        return NotFound();
//    }
//    return (Application)retrievedResult.Result;
//}


//[HttpPost]
//public async Task<IActionResult> CreateAsync(Application item)
//{
//    Application tmp = new Application()
//    {
//        PartitionKey = item.PartitionKey,
//        RowKey = "",
//        address = item.address,
//        name = item.name,
//        phone = item.phone,
//        count_child = item.count_child,
//        count_person = item.count_person,
//        count_veg = item.count_veg,
//        message = item.message

//    };

//    // Create the TableOperation that inserts the customer entity.
//    TableOperation insertOperation = TableOperation.Insert(tmp);

//    // Execute the insert operation.
//    await applicationTable.ExecuteAsync(insertOperation);

//    return Ok(item);
//}
