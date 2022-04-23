namespace ShoppingSiteBusiness;

public interface IAuthentication{
    string UserName{get;set;}
    string passWord{set;}
    public bool IsUserExist(string Username);
    public void SaveToDataBase(string NewUsername,string NewPassword);
     public bool LoginUser();

}

public interface IValidation{
    public bool ValidatePassword(string passWord,string Repassword);
    public  bool ValidateMobilNo(string PhoneNo);
    public  bool ValidateUserName(string UserName);
    public  bool ValidateEmail(string Email);
}

public interface IDisplay{
    public List<CartItem> ShowCategoryItem();
    
}