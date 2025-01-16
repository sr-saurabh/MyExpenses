export const getIconForCategory = (categoryName: string) => {
    switch (categoryName) {
        case 'Food':
            return 'money-bill'
        case 'Transportation':
            return 'car'
        case 'Entertainment':
            return 'ticket'
        case 'Housing':
            return 'building'
        case 'Shopping':
            return 'shopping-bag'
        default:
            return 'objects-column'
    }
}