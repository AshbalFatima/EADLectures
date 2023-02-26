namespace VS_APis.DTOs
{
    public record CreateCustomer(string name,string email,string contactNumber);
    public record UpdateCustomer(Guid id, string name, string email, string contactNumber,bool IsActive);
}
