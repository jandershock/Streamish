const baseUrl = '/api/video';

export const getAllVideos = () => {
  return fetch(baseUrl)
    .then((res) => res.json())
};

export const getAllVideosWithComments = () => {
  return fetch(baseUrl + "/get-with-comments")
    .then((res) => res.json())
};

export const addVideo = (video) => {
  return fetch(baseUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(video),
  });
};

export const search = (query, sortDesc = '') => {
  return fetch(baseUrl + "/search?" + `q=${query}` + `${sortDesc != '' ? `&sortDesc=${sortDesc}` : ''}`)
    .then(res => res.json())
}

export const getVideo = (id) => {
  return fetch(`${baseUrl}/GetByIdWithComments/${id}`).then((res) => res.json());
};
