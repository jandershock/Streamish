import { Card, CardTitle, CardText } from "reactstrap";

const VideoComment = ({ comment }) => {


    return (
        <Card>
            <CardTitle>
                User Profile Id: {comment.userProfileId}
            </CardTitle>
            <CardText>
                {comment.message}
            </CardText>
        </Card>
    )
}

export default VideoComment;