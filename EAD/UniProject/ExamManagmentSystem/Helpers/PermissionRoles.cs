namespace ExamManagmentSystem.Helpers
{
    public static class RolePermissions
    {
        public static List<string> AllPermissions = new()
        {
            // Clerk
            "GenerateAttendanceSheetPdf",
            "GenerateSittingPlanPdf",

            // Admin
            "ManageStudents",
            "GeneratePdfs",
            "ManageExamSheets",
            "AddBatch",
            "ManageRooms",
            "SearchStudent",

            // SuperAdmin
            "ManageRoles"
        };

        public static List<string> ClerkPermissions => new()
        {
            "GenerateAttendanceSheetPdf",
            "GenerateSittingPlanPdf","ManageExamSheets","GeneratePdfs"
        };

        public static List<string> AdminPermissions => new()
        {
            "GenerateAttendanceSheetPdf",
            "GenerateSittingPlanPdf",
            "ManageStudents",
            "GeneratePdfs",
            "AddBatch",
            "ManageRooms",
            "SearchStudent","ManageExamSheets"
        };

        public static List<string> SuperAdminPermissions => new()
        {
            "GenerateAttendanceSheetPdf",
            "GenerateSittingPlanPdf",
            "ManageStudents",
            "GeneratePdfs",
            "AddBatch",
            "ManageRooms",
            "SearchStudent",
            "ManageRoles","ManageExamSheets"
        };
    }


}
