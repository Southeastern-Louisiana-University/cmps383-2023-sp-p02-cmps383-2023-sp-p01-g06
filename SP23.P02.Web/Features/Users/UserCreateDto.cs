namespace SP23.P02.Web.Features.Users
{
    public class UserCreateDto
    {
        public string UserName { get; set; }
        public List<String> Roles { get; set; }
        public string Password { get; set; }
    }
}
