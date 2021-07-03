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
        icon: 'mdi-pencil',
        route: '/path1',
        title: 'Path 1'
    },
    {
        icon: 'mdi-pencil',
        route: '/path2',
        title: 'Path 2'
    },
]
