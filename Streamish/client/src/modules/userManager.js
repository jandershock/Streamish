const baseUrl = '/api/userprofile';

export const getVideosForUser = (id) => {
    return fetch(`${baseUrl}/${id}/AuthoredVideos`)
        .then((res) => res.json());
};