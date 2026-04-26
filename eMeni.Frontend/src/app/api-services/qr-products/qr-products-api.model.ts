import { BasePagedQuery } from '../../core/models/paging/base-paged-query';
import { PageResult } from '../../core/models/paging/page-result';

export class ListQrProductsRequest extends BasePagedQuery {
  constructor() {
    super();
    this.paging.pageSize = 20;
  }
}

export interface ListQrProductDto {
  id: number;
  productName: string;
  description: string;
  price: number;
  imageUrl: string;
  materialType: string;
  size: string;
}

export type ListQrProductsResponse = PageResult<ListQrProductDto>;
