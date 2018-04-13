using System;

namespace BO
{
    public class ApiQueries
    {
        public string TABLE_NAME { get; private set; }
        public string SAVE_SP { get; private set; }
        public string GET_ALL_SP { get; private set; }
        public string SQL_DELETE_ONE { get; private set; }
        public string SQL_GET_ONE { get; private set; }
        public string UPDATE_SP { get; private set; }

        public string SQL_DELETE_CHILDREN { get; private set; }
        public string SQL_GET_CHILDREN { get; private set; }
        public ApiQueries(string tableName, string parentCol = "")
        {
            TABLE_NAME = tableName;
            SAVE_SP = string.Format("SAVE_{0}", TABLE_NAME);
            GET_ALL_SP = string.Format("GET_ALL_{0}", TABLE_NAME);
            SQL_DELETE_ONE = string.Format("DELETE FROM {0} WHERE Id = @id", TABLE_NAME);
            SQL_GET_ONE = string.Format("SELECT * FROM {0} WHERE Id = @id", TABLE_NAME);
            UPDATE_SP = string.Format("UPDATE_{0}", TABLE_NAME);

            if (!string.IsNullOrEmpty(parentCol))
            {
                SQL_DELETE_CHILDREN = string.Format("DELETE FROM {0} WHERE {1} = @id", TABLE_NAME, parentCol);
                SQL_GET_CHILDREN = string.Format("SELECT * FROM {0} WHERE {1} = @id", TABLE_NAME, parentCol);
            }
        }
    }
}
