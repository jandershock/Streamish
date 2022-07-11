import React from "react";
import VideoComment from "./VideoComment";
import { Link } from "react-router-dom";
import { Card, CardBody, CardHeader } from "reactstrap";

const Video = ({ video }) => {
  return (
    <Card className="text-center">
      <p className="px-2">Posted by: {video.userProfile.name}</p>
      <CardBody>
        <iframe className="video"
          src={video.url}
          title="YouTube video player"
          frameBorder="0"
          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
          allowFullScreen />

        <p>
          <Link to={`/videos/${video.id}`}>
            <strong>{video.title}</strong>
          </Link>
        </p>
        <p>{video.description}</p>
        <br></br>
        {video.comments.length > 0 &&
          <Card>
            <CardHeader>
              <h5>Comments</h5>
            </CardHeader>
            <CardBody>
              {video.comments.map(comment => (
                <VideoComment key={comment.id} comment={comment}></VideoComment>
              ))}
            </CardBody>
          </Card>
        }
      </CardBody>
    </Card>
  );
};

export default Video;