export interface PageHeaderAction {
    icon: string
    disabled?: () => boolean
    label: string
    loading?: () => boolean
    variant?: 'primary' | 'accent' | 'error' | 'success'
    type?: 'flat' | 'outlined' | 'text' | 'raised'
    action: () => void
}
