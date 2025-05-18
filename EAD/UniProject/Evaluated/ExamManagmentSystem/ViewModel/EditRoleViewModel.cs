namespace ExamManagmentSystem.ViewModel
{
    public class EditRolePermissionsViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public List<PermissionCheckbox> Permissions { get; set; } = new();
    }

    public class PermissionCheckbox
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }

}
