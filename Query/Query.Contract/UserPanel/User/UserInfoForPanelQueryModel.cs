using Shared.Domain.Enum;

namespace Query.Contract.UserPanel.User;

public class UserInfoForPanelQueryModel
{
    public UserInfoForPanelQueryModel(string fullName, string mobile, string email, 
         Gender userGender, int transactionCount, int transactionSum, string creationDate)
    {
        FullName = fullName;
        Mobile = mobile;
        Email = email;
        UserGender = userGender;
        TransactionCount = transactionCount;
        TransactionSum = transactionSum;
        CreationDate = creationDate;
    }

    public string FullName { get; private set; }
    public string Mobile { get; private set; }
    public string Email { get; private set; }
    public Gender UserGender { get; private set; }
    public int TransactionCount { get; private set; }
    public int TransactionSum { get; private set; }
    public string CreationDate { get; private set; }
}