class UploadImagen
{
    constructor() { }

    archivo(evt, id) {

        let file = evt.target.files;//fileslist object
        let f = file[0];
        if (f.type.match('image.*'))
        {
            let reader = new FileReader();
            reader.onload = ((theFile) => {

                return (e) => {

                    document.getElementById(id).innerHTML = ['<img class="responsive-img" src="', e.target.result, '" tilte="',
                                                            escape(theFile.name), '"/>'].join('');

                };

            })(f);
            reader.readAsDataURL(f);
        }

    }
}