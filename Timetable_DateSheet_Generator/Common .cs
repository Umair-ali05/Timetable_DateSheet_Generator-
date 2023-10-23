using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator
{
    public struct Colors
    {
        public int CourseID;
        public int Color;
    }
    public class Common
    {
        public static List<Colors> Colors = new List<Colors>();
        public static readonly Random _random = new Random();

        public static string NotFound = "Record Not Found!.";
        /// Wrong
        public static string Fail = "Something went wrong!.";
        /// URL
        public static string returnUrl = null;
        /// Error 
        /// 
        public static string Error = "error";
        /// Success
        /// 
        public static string Success = "success";
        /// Warning
        /// 
        public static string Warning = "warning";
        /// Information
        /// 
        public static string Information = "information";

        /// Insert Fail
        /// 
        public static string InsertFail = "Record is not inserted!";
        /// Update Fail
        /// 
        public static string UpdateFail = "Record is not updated!";
        /// Delete Fail
        /// 
        public static string DeleteFail = "Record is not Deleted!";
        /// Already Exists 
        /// 
        public static string AlreadyExists = "Record Already Exists!";
        /// Get Fail
        /// 
        public static string GetFail = "Record is not Retrieved!";
        /// Get List Fail
        /// 
        public static string GetListFail = "Records is not Retrieved!";

        /// Insert Success
        /// 
        public static string InsertSuccess = "Record is inserted successfully!";
        /// Update Success
        /// 
        public static string UpdateSuccess = "Record is updated successfully!";
        /// Delete Success
        /// 
        public static string DeleteSuccess = "Record is deleted successfully!";
        /// Get Success
        /// 
        public static string GetSuccess = "Record is retrieved successfully!";
        /// Get List Success
        /// 
        public static string GetListSuccess = "Records is Retrieved Successfully!";
    }
}
