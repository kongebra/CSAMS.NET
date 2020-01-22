namespace CSAMS.Course.Queries {
    public class GetCourseInfoQuery {
        public string Code { get; set; }

        public GetCourseInfoQuery(string code) {
            this.Code = code;
        }
    }
}
