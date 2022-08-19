using POC_ConsumeAPI.Data;


namespace POC_ConsumeAPI.Services.IServices
{

    /// <summary>
    /// IServices interface to implement the Services Pattern
    /// </summary>
    public interface IServices
    {
        #region Property 

        /// <summary>
        ///  Get all item from the list
        /// </summary>
        /// <returns></returns>
        Task<List<ToDo>> GetAllAsync();

        /// <summary>
        ///  Get item by id from the list 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ToDo> GetAsync(int id);

        /// <summary>
        /// Create new item in the list
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(ToDo model);

        /// <summary>
        /// Update given item in the list
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(ToDo model);

        /// <summary>
        /// Delete given item from the list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);

        #endregion
    }

}

