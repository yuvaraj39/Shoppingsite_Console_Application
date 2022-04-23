namespace ShoppingSiteBusiness;

public class CartItem{

     public CartItem(string categortype, int categoryid, string categoryname, int itemId,string itemname, string description,decimal itemprice,int stock,int imgid)
    {
        this.categortype = categortype;
        this.categoryid = categoryid;
        this.categoryname = categoryname;
        this.itemId = itemId;
        this.itemname = itemname;
        this.description = description;
        this. itemprice =  itemprice;
        this.stock = stock;
        this.imgid =imgid;
    }
    private string categortype;
    private int categoryid;

    private string categoryname;

    private int itemId;

    private string itemname;

    private string description;

    private decimal itemprice;
    private int stock;

    private int imgid;

    public string Categortype
    {
        get { return categortype;  }
        set { categortype = value; }
    }
  
    public int CategoryId
    {
        get { return categoryid; }
        set { categoryid = value; }
    }
    public string CategoryName
    {
        get { return categoryname; }
        set { categoryname = value; }
    }
    public int ItemId
    {
        get { return itemId; }
        set {itemId = value; }
    }
    public string ItemName
    {
        get { return itemname; }
        set { itemname = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    public decimal ItemPrice
    {
        get { return itemprice; }
        set {itemprice = value; }
    }
    public int Stock
    {
        get { return stock; }
        set { stock = value; }
    }

     public int ImgId
    {
        get { return imgid; }
        set { imgid = value; }
    }

}
