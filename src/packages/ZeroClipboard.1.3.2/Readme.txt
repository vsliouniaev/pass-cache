For detailed instructions, visit https://github.com/zeroclipboard/zeroclipboard


Sample:

<script src="@Url.Content("~/Scripts/ZeroClipboard.min.js")"></script>
<script>
    $(function (){
        var clip = new ZeroClipboard(document.getElementById("copy-button"), {
          moviePath: "/path/to/ZeroClipboard.swf"
        });

        clip.on('load', function(client){
          // alert("movie is loaded" );
        });

        clip.on('complete', function(client, args){
          this.style.display = 'none'; // "this" is the element that was clicked
          alert("Copied text to clipboard: " + args.text );
        });
    });
</script>