namespace CSAMS.Course.Commands {

    public class DeleteCourseCommand {
        public string Code { get; set; }

        public DeleteCourseCommand(string code) {
            this.Code = code;
        }
    }
}