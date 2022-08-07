export class ServiceModel {
    serviceId?: number;
    vendorId: number;
    serviceCategoryId: number;
}

export class ItemModel {
    service: ServiceCategory;
    quantity: number;
    date: Date;
}

export class ServiceCategory {
    serviceCategoryId: number;
    name: string;
    quantity?: number;
    date?: Date;
}

export class BidResponseModel {
    bidId: number;
    cartId: number;
    vendorId: number;
    amount: number;
}

export class CartResponseModel {
    cartId: number;
    orderId: number;
    statusId: number;
    serviceCategoryId: number;
    rate: number;
    quantity: number;
    date: Date;
    serviceCategory: ServiceCategory;
    bids: BidResponseModel[];
}


export class OrderResponseModel {
    items: CartResponseModel[];
    userId?: number;
    name: string;
    email: string;
    addressLine1: string;
    addressLine2: string;
    city: string;
    state: string;
    pincode: string;
    phone: string;
}