import React, { useEffect, useState } from "react";
import Video from './Video';
import VideoForm from "./VideoForm";
import { getAllVideos, getAllVideosWithComments, search } from "../modules/videoManager";
import { Button, Input } from "reactstrap";

const VideoList = () => {
  const [videos, setVideos] = useState([]);
  const [searchValue, setSearchValue] = useState([]);

  const getVideos = () => {
    getAllVideosWithComments().then(videos => setVideos(videos));
  };

  const submitSearch = () => {
    search(searchValue).then(videos => setVideos(videos));
  };

  const handleChange = (event) => {
    setSearchValue(event.target.value);
  };

  useEffect(() => {
    getVideos();
  }, []);

  return (
    <div className="container">
      <div className="row py-2">
        <div className="col-md-6 offset-md-3 col-sm-8 offset-sm-2 col-12">
          <VideoForm getVideos={getVideos}></VideoForm>
        </div>
      </div>
      <div className="row py-2">
        <div className="col-10 px-0">
          <Input onChange={handleChange}></Input>
        </div>
        <div className="col-2 px-0">
          <Button onClick={submitSearch} block>Search</Button>
        </div>
      </div>
      <div className="row justify-content-center px-0">
        {videos.map((video) => (
          <Video video={video} key={video.id} />
        ))}
      </div>
    </div>
  );
};

export default VideoList;