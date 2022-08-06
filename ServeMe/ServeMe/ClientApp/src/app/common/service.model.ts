export class ServiceModel {
    serviceId: number;
    vendorId: number;
    serviceCategoryId: number;
    quantity: number;
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