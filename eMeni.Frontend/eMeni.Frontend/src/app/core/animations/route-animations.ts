import { trigger, transition, style, query, animate, group } from '@angular/animations';

/**
 * Route animation for smooth transitions between routes
 * Uses fade and slide effects for a polished user experience
 */
export const routeAnimations = trigger('routeAnimations', [
  // Transition from any route to any route
  transition('* <=> *', [
    // Set initial styles for both entering and leaving views
    query(':enter, :leave', [
      style({
        position: 'absolute',
        left: 0,
        top: 0,
        width: '100%',
        opacity: 0
      })
    ], { optional: true }),
    
    // Group animations for smooth transition
    group([
      // Animate the leaving view out (fade and slide left)
      query(':leave', [
        animate('400ms ease-in-out', style({ 
          opacity: 0, 
          transform: 'translateX(-30px) scale(0.98)' 
        }))
      ], { optional: true }),
      
      // Animate the entering view in (fade and slide from right)
      query(':enter', [
        style({ 
          transform: 'translateX(30px) scale(0.98)',
          opacity: 0 
        }),
        animate('400ms ease-in-out', style({ 
          opacity: 1, 
          transform: 'translateX(0) scale(1)' 
        }))
      ], { optional: true })
    ])
  ])
]);

/**
 * Fade animation for component transitions
 */
export const fadeAnimation = trigger('fadeAnimation', [
  transition(':enter', [
    style({ opacity: 0 }),
    animate('400ms ease-out', style({ opacity: 1 }))
  ]),
  transition(':leave', [
    animate('300ms ease-in', style({ opacity: 0 }))
  ])
]);

/**
 * Slide in from right animation
 */
export const slideInRight = trigger('slideInRight', [
  transition(':enter', [
    style({ transform: 'translateX(100%)', opacity: 0 }),
    animate('400ms ease-out', style({ transform: 'translateX(0)', opacity: 1 }))
  ]),
  transition(':leave', [
    animate('400ms ease-in', style({ transform: 'translateX(-100%)', opacity: 0 }))
  ])
]);

/**
 * Slide in from left animation
 */
export const slideInLeft = trigger('slideInLeft', [
  transition(':enter', [
    style({ transform: 'translateX(-100%)', opacity: 0 }),
    animate('400ms ease-out', style({ transform: 'translateX(0)', opacity: 1 }))
  ]),
  transition(':leave', [
    animate('400ms ease-in', style({ transform: 'translateX(100%)', opacity: 0 }))
  ])
]);

/**
 * Scale and fade animation
 */
export const scaleFade = trigger('scaleFade', [
  transition(':enter', [
    style({ transform: 'scale(0.96)', opacity: 0 }),
    animate('400ms cubic-bezier(0.25, 0.8, 0.25, 1)', style({ transform: 'scale(1)', opacity: 1 }))
  ]),
  transition(':leave', [
    animate('300ms ease-in', style({ transform: 'scale(0.96)', opacity: 0 }))
  ])
]);

