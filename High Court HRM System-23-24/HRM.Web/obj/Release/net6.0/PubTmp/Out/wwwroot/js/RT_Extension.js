

window.onload = function () {
    if (window.jQuery) {
        console.log("RT_Extension Loaded");
        //Function to preview Image
        function readURL(input, imgId) {
            console.log("RT_Extension Preview Function Call");
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    console.log("Image ID is " + imgId);
                    $('#'+imgId).attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }

        $(".customImageUpload").change(function () {

            var id = $(this).attr('id');
            console.log("RT_Extension Preview Mapped");
            readURL(this, "_image" + id);
        });
        //Function Preview Image ended...

        var imageModal = $('#customImage');
        if (imageModal == null) {
            
           
            

        }
        $('.date').datepicker({
            autoHide: true,
            format: 'dd-M-yyyy',

            endDate: new Date()
        }).on('changeDate', function (e) {
            $(this).datepicker('hide');
        });

    } else {
        // jQuery is not loaded
        console.log("RT_Extension Required JQUERY");
        const btn = document.createElement("button");
        btn.innerHTML = "RT Extension JQUERY Required!";
        btn.id = 'rt_extension_button';
        document.body.appendChild(btn);
    }
   
}

function customImageAction(id) {
    $('#customImageModalImage').attr('src', $('#' + id).attr('src'));
    $('#customImageModal').modal('show');

}
//String '20-May-2022' to date
function parseDate(s) {
    var months = {
        jan: 0, feb: 1, mar: 2, apr: 3, may: 4, jun: 5,
        jul: 6, aug: 7, sep: 8, oct: 9, nov: 10, dec: 11
    };
    var p = s.split('-');
    return new Date(p[2], months[p[1].toLowerCase()], p[0]);
}
function getFormattedDate(date) {
    var year = date.getFullYear();

    var month = (1 + date.getMonth()).toString();
    month = month.length > 1 ? month : '0' + month;

    var day = date.getDate().toString();
    day = day.length > 1 ? day : '0' + day;

    return month + '/' + day + '/' + year;
}
function TextChange(srcId, id) {
    console.log("Copy");
    var r = $('#' + srcId).val();
    console.log(r);
    //var d = parseDate(r);
    //var t = getFormattedDate(d);
    $('#' + id).attr('value', r);
    console.log($('#' + id).val());
}