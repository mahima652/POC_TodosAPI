using POC_ConsumeAPI.Model;

namespace POC_ConsumeAPI.Data
{
    /// <summary>
    /// Model for ToDo Entity
    /// </summary>
    public class ToDo
    {
        #region Property

        /// <summary>
        /// Get/Set for Id 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Get/Set for UserId
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// Get/Set for Title
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Get/Set for Completed
        /// </summary>
        public bool completed { get; set; }

        #endregion

    }

}
