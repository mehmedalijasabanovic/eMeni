import { BasePagedQuery } from '../../core/models/paging/base-paged-query';
import { PageResult } from '../../core/models/paging/page-result';

export class ListBusinessesRequest extends BasePagedQuery {
  city?: string;
  categoryId?: number;
}

export class Promotion {
  static readonly Premium = 1;
  static readonly Basic = 2;
}

export type PromotionRank = (typeof Promotion)[keyof typeof Promotion];

export interface ListBusinessesDto {
  id: number;
  businessName?: string;
  description?: string;
  address?: string;
  promotionRank?: PromotionRank;
}

export type ListBusinessesResponse = PageResult<ListBusinessesDto>;
