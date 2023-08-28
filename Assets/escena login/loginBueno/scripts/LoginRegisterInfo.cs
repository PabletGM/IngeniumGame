
public class LoginRegisterInfo
{
    //variables

    //userName
    private string userName;
    //company
    private string company;
    //mail
    private string email;
    //first name
    private string firstName;
    //lastName
    private string lastName;
    //age
    private string age;
    //password
    private string password;
    //confirm password
    private string confirmPassword;

    //constructor
    public LoginRegisterInfo(string userName, string company, string email, string firstName,string lastName,string age,string password, string confirmPassword)
    {
        this.userName = userName;
        this.company = company;
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
        this.password = password;
        this.confirmPassword = confirmPassword;
    }
}
