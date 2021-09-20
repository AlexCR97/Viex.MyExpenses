export type SizeVariant = 'xs' | 'sm' | 'md' | 'lg' | 'xl'

const ButtonSizeMap = new Map<SizeVariant, string>([
    [ 'xs', '30px' ],
    [ 'sm', '40px' ],
    [ 'md', '50px' ],
    [ 'lg', '60px' ],
    [ 'xl', '70px' ],
])

export function getButtonSize(size: SizeVariant) {
    return ButtonSizeMap.get(size)
}
