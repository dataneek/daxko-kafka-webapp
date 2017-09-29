// Write your Javascript code.
function getPostData(term) {
  return {
    suggest: {
      member_suggest: {
        prefix: term,
        completion: {
          field: "suggest"
        }
      }
    }
  };
}

function getMemberDocs(data) {
  return $.map(data.suggest.member_suggest[0].options, function(item) {
    return item._source.FirstName + ' ' + item._source.LastName;
  });
}

var members = new Bloodhound({
  queryTokenizer: Bloodhound.tokenizers.whitespace,
  datumTokenizer: Bloodhound.tokenizers.whitespace,
  remote: {
    url: "http://localhost:9200/members/member/_search",
    prepare: function(query, settings) {
      // console.log(query);
      settings.type = "POST";
      settings.data = JSON.stringify(getPostData(query));
      settings.contentType = "application/json; charset=UTF-8";
      // console.log(settings);
      return settings;
    },
    transform: function(response) {
      var data = getMemberDocs(response);
      console.log(data);
      return data;
    }
  }
});

$('.typeahead').typeahead({
  minLength: 2,
  highlight: true
}, {
  source: members,
  name: 'members'
});