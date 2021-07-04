export interface SideNavItem {
    icon: string;
    route: string;
    title: string;
}

export const DefaultSideNavItems: SideNavItem[] = [
    {
        icon: 'mdi-pencil',
        route: '/home',
        title: 'Home'
    },
    {
        icon: 'mdi-code-tags',
        route: '/developer',
        title: 'Developer'
    },
]
