using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Dapper;
using System.Data;

namespace WebApi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IDapper _dapper;

        public PersonController(IDapper dapper)
        {
            _dapper = dapper;
        }

        [Route("api/person/getPerson")]
        [HttpGet]
        public async Task<Person> getById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<Person>($"SELECT * FROM [Dummy] WHERE Id = {Id}", null, System.Data.CommandType.Text));
            return result;
        }

        [Route("api/person/Del")]
        [HttpDelete]
        public int DelPerson(int id)
        {
            //var result = await Task.FromResult (_dapper.Execute($"Update [Dummy] set name= where ID = {id}", null, System.Data.CommandType.Text));


            int result = _dapper.Execute($"DELETE from [Dummy] where ID = {id}", null, CommandType.Text);
            return result;
        }


        [Route("api/person/Edit")]
        [HttpPut]
        public async Task<int> EditPerson(Person person)
        {
            //DynamicParameters param = new DynamicParameters(new { name = person.Name, age = person.Age, id = person.Id });
            DynamicParameters param = new DynamicParameters();
            param.Add("name", person.Name);
            param.Add("age", person.Age);
            param.Add("id", person.Id);
            var result = await Task.FromResult(_dapper.Update<int>("SP_Dummy__Update",
                param,
                CommandType.StoredProcedure));
            return result;
        }
        [Route("api/person/GetAll")]
        [HttpGet]
        public async Task<List<Person_Full>> Get_All_Person()
        {

            var result = await Task.FromResult(_dapper.GetAll<Person_Full>("SP_Get_List_Person", new DynamicParameters { }, CommandType.StoredProcedure));
                return result;
        }

        
    }
}
