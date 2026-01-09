import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-cards-skeleton',
  standalone: false,
  templateUrl: './cards-skeleton.html',
  styleUrl: './cards-skeleton.scss',
})
export class CardsSkeleton {
  @Input() cards: number = 6; // Default to 6 cards (2 rows of 3)

  get cardsArray(): number[] {
    return Array(this.cards).fill(0).map((_, i) => i);
  }
}
