export interface ProductsInterface {
    idProduct:      number
    code:           string;
    description?:   string;
    price:          number;
    image:          string;
    stock:          number;
    IsAvailable:    boolean;
}