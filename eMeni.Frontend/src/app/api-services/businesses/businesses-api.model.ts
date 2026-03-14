import { BasePagedQuery } from '../../core/models/paging/base-paged-query';
import { PageResult } from '../../core/models/paging/page-result';

export class ListBusinessesRequest extends BasePagedQuery {
  city?: string;
  /**
   * Business category id.
   * Sent from public layout when user clicks "pogledaj biznise".
   */
  categoryId?: number;
}

export interface ListBusinessesDto {
  id: number;
  businessName?: string;
  description?: string;
  address?: string;
  promotionRank?: number;
}

export type ListBusinessesResponse = PageResult<ListBusinessesDto>;

