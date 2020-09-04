function resetForm() {
    //$(this).closest('form-control').find("input[type=text], textarea").val("");
    $("input.form-control").val("");
    // This is a global function and is a part of window object.
    // This can be called from anywhere once the file is loaded.
}