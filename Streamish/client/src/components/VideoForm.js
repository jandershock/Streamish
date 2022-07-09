import { Form, FormGroup, Label, Input, Button, FormFeedback } from "reactstrap";
import * as vm from "../modules/videoManager";

const VideoForm = () => {
    const handleSubmit = (event) => {
        vm.addVideo({
            title: event.target.elements["videoTitle"].value,
            description: event.target.elements["videoDescription"].value,
            url: event.target.elements["videoUrl"].value
        });
    }

    return (
        <Form onSubmit={handleSubmit} className="text-left">
            <h2 className="text-center">New Video Form</h2>
            <FormGroup>
                <Label for="videoTitle">
                    Title:
                </Label>
                <Input id="videoTitle" type="text" required></Input>
            </FormGroup>
            <FormGroup>
                <Label for="videoDescription">
                    Description:
                </Label>
                <Input id="videoDescription" type="text"></Input>
            </FormGroup>
            <FormGroup>
                <Label for="videoUrl">
                    Url:
                </Label>
                <Input id="videoUrl" type="text" required></Input>
            </FormGroup>
            <div className="text-right">
                <Button>
                    Add Video
                </Button>
            </div>
        </Form>
    )
}

export default VideoForm;