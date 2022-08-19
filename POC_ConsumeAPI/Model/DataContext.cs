using POC_ConsumeAPI.Data;

namespace POC_ConsumeAPI.Model
{
    /// <summary>
    /// This class is used to stored the list from 3rd party and pass to the local list as well
    /// </summary>
    public class DataContext
    {
        #region Property

        /// <summary>
        /// This property is used as shared data between the Todo and TodoLocal 
        /// </summary>
        public static List<ToDo>  dataCxt { get; set; }

        #endregion 

    }
}
