$(document).ready(function () {
    $('#searchInput').on('keyup', function () {
        var query = $(this).val();

        if (query.length > 2) { // Trigger search only if input length is more than 2 characters
            $.get('/Home/Search', { searchString: query }, function (data) {
                $('#searchResults').empty();

                if (data.length > 0) {
                    $.each(data, function (index, product) {
                        $('#searchResults').append(
                            '<li class="list-group-item">' +
                            '<strong>' + product.name + '</strong><br>' +
                            '<span>' + product.description + '</span>' +
                            '</li>'
                        );
                    });
                } else {
                    $('#searchResults').append('<li class="list-group-item">No results found</li>');
                }
            });
        } else {
            $('#searchResults').empty(); // Clear results if input length is less than 3 characters
        }
    });
});
