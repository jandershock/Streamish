import React from "react";
import VideoComment from "./VideoComment";
import { Card, CardBody, CardHeader } from "reactstrap";

const Video = ({ video }) => {
  return (
    <Card >
      <p className="text-left px-2">Posted by: {video.userProfile.name}</p>
      <CardBody>
        <iframe className="video"
          src={video.url}
          title="YouTube video player"
          frameBorder="0"
          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
          allowFullScreen />

        <p>
          <strong>{video.title}</strong>
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
                  <VideoComment comment={comment}></VideoComment>
                ))}
              </CardBody>
            </Card>
          }
      </CardBody>
    </Card>
  );
};

export default Video;