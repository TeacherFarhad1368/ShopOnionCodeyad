﻿namespace Query.Contract.UI.Product;

public class ProductShopUiQueryModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string ImageName { get; set; }
    public string ImageAlt { get; set; }
    public string Shop { get; set; }
    public int Price { get; set; }
    public int PriceAfterOff { get; set; }
}