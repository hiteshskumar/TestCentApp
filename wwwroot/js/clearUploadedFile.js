function clearUploadedFile() {
    var input = $("input[type='file']");
    input.html(input.html()).val('');
}
function ClearUploadedFileByName(filename) {
    var input = $("input[type='file']");
    const dt = new DataTransfer();
    for(let i=0; i<input[0].files.length; i++)
    {
        var file = input[0].files[i];
        if(file.name != filename)
        {
            dt.items.add(file);
        }
    }
    input[0].files = dt.files;
}