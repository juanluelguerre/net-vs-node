export interface user {
    id: number,
    first_name: string,
    last_name: string,
    email: string,
    avatar: string
}

export interface pagedUsers {
    total: number,
    page: number,
    per_page: number,
    total_pages: number,
    data: user[]
}

