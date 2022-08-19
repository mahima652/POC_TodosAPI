using Newtonsoft.Json;
using POC_ConsumeAPI.Data;
using POC_ConsumeAPI.ExceptionTYpe;
using POC_ConsumeAPI.Model;
using POC_ConsumeAPI.Services.IServices;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;


namespace POC_ConsumeAPI.Helper
{
    /// <summary>
    ///  This class is used to perform the CRUD operation for locally present list 
    /// </summary>
    public class TodoLocalServices : ITodoLocalServices
    {
        #region Property

        private static List<ToDo> result;

        #endregion 

        #region HTTP Method

        /// <summary>
        /// Get all the list which is present locally
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<ToDo>> GetAllAsync()
        {
            result = Data.DataContext.dataCxt;
            if(result != null)
            {
                return result;
            }
            throw new Exception("Internal server error");
        }

        /// <summary>
        /// Get the list by id for locallist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<ToDo> GetAsync(int id)
        {
            CheckList();
            var resultModel = result.FirstOrDefault(x => x.id == id);
            if (resultModel != null)
            {
                return resultModel;
            }
            throw new NotFoundException("Requested Id not found ");
        }

        /// <summary>
        /// Add new item in the locally present list
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="InvalidException"></exception>
        public async Task<bool> CreateAsync(ToDo model)
        {
            CheckList();
            var updatedModel = result.FirstOrDefault(x => x.id == model.id);
            if (updatedModel == null)
            {
                result.Add(model);
                return true;
            }
            throw new InvalidException("Requested Item is already present ");
        }

        /// <summary>
        /// Update the item which is present locally 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<bool> UpdateAsync(ToDo model)
        {
            CheckList();
            var updatedModel = result.FirstOrDefault(x => x.id == model.id);
            if (updatedModel != null)
            {
                updatedModel.id = model.id;
                updatedModel.userId = model.userId;
                updatedModel.completed = model.completed;
                updatedModel.title = model.title;
                return true;
            }
            throw new NotFoundException("Requested Id not found ");
        }

        /// <summary>
        ///  Delete the item which is present locally 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<bool> DeleteAsync(int id)
        {
            CheckList();
            var deletedmodel = result.FirstOrDefault(x => x.id == id);
            if (deletedmodel != null)
            {
                result.Remove(deletedmodel);
                return true;
            }
            throw new NotFoundException("Requested Id not found ");

        }

        #endregion

        #region Internal method 

        /// <summary>
        ///  This method is used to check list is present locally or not
        /// </summary>
        /// <exception cref="InvalidException"></exception>
        private void CheckList()
        {
            if (result == null)
            {
                Console.WriteLine("List is not present locally");
                throw new InvalidException("List is not present locally ," +
                   " First need to fetch the list from Todos");
            }
        }

        #endregion
    }
}
