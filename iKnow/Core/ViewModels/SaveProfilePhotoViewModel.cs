namespace iKnow.Core.ViewModels {
    public class SaveProfilePhotoViewModel {
        public SaveProfilePhotoViewModel(string userId, string dataURL) {
            UserId = userId;
            DataUrl = dataURL;
        }

        public string UserId { get; private set; }
        public string DataUrl { get; private set; }
    }
}