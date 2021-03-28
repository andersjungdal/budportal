namespace ModelsInterfaces
{
    public interface IUserSubmit<TCompany, TXmlTemplate> : IUser<TCompany, TXmlTemplate> where TCompany : ICompany<TXmlTemplate> where TXmlTemplate : IXmlTemplate
    {
        string Password { get; set; }
    }
}