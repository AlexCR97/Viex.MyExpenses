import { StaticRoutes } from "src/app/Routes";

export interface SideNavigationItem {
    icon: string
    label: string
    path?: string
    click?: () => void
}

export const HomeSideNavigationItem: SideNavigationItem = {
    icon: 'house',
    label: 'Home',
    path: '/app/home',
};

export const TransactionsSideNavigationItem: SideNavigationItem = {
    icon: 'currency-dollar',
    label: 'Transactions',
    path: '/app/transactions',
};

export const SettingsSideNavigationItem: SideNavigationItem = {
    icon: 'gear',
    label: 'Settings',
    path: '/app/settings',
};

export const UsersSideNavigationItem: SideNavigationItem = {
    icon: 'people',
    label: 'Users',
    path: StaticRoutes.usersPage,
}

export const DefaultSideNavigationItems = [
    HomeSideNavigationItem,
    TransactionsSideNavigationItem,
    SettingsSideNavigationItem,
    UsersSideNavigationItem,
]

