import { ColorVariant } from "src/app/types/ColorVariant.type";

export interface ActionDrawerItem {
    icon: string
    label: string
    variant?: ColorVariant
    action: () => void
}
