$(document).ready(function () {
    $('#search-button').on('submit', function (event) {
        event.preventDefault();          

        let searchString = $('input[name="searchString"]').val();         

        $.ajax({
            url: '/Product/Search',            
            method: 'GET',
            data: { searchString: searchString },     
            success: function (response) {
                $('#searchResults').html(response) 
            },

            error: function () {
                $('#searchResults').html('<p>An error occurred while processing your request.</p>');
            }
        });
    });
});

