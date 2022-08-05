export class ServiceModel {
    serviceID: number;
    rateType: number;
    rate: number;
    name: string;
    vendorId: number;
    serviceCategoryId: number;
    quantity: number;
}

export class ItemModel {
    service: ServiceModel;
    quantity: number;
    date: Date;
    rate: number;
}