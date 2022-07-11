import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getVideosForUser } from "../modules/videoManager";
import Video from './Video';

const UserDetails = () => {
    const { id } = useParams();

    const [videoList, setVideoList] = useState();

    useEffect(() => {
        getVideosForUser(id).then(r => setVideoList(r.authoredVideos));
    }, []);

    if (!videoList) {
        return null;
      }

    return (
        <div className="container">
            <div className="row justify-content-center px-0">
                {videoList.map((video) => (
                    <Video video={video} key={video.id} />
                ))}
            </div>
        </div>
    )
}


export default UserDetails;