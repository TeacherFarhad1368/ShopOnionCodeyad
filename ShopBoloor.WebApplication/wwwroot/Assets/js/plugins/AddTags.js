if(typeof($add)=="undefined")var $add={version:{},auto:{disabled:false}};(function($){
  $add.version.Tags = "1.0.0";
  $add.Tags = function(selector, settings){
    var r = $(selector).map(function(i, input){
      var $i = $(input);
      var S = $.extend($i.data(), settings);
      if($i.attr("name")) S.name = $i.attr("name");
      if($i.attr("class")) S.class = $i.attr("class");
      if($i.attr("id")) S.id = $i.attr("id");
      if($i.attr("placeholder")) S.placeholder = $i.attr("placeholder");

      var o = new $add.Tags.Obj($i.val(), S);
      o.render($i, "replace");

      return o;
    });
    return (r.length==0)?null:(r.length==1)?r[0]:r;
  };
  $add.Tags.Obj = Obj.create(function(tags, settings){
    this.defSettings({
      name: "",
      class: "",
      id: "",
      placeholder: "",
      limit: 0,
      tab: false,
      space: false,
      comma: false,
      enter: false,
      backspace: true,
      duplicates: false,
      removable: true,
      delimiter: ",",
      casesensitive: false,
      autohash: false,
      valuehash: false
    });
    this.defMember("tags", [], function(newTags){
      this.addTag(newTags);
    });
    Object.defineProperty(this, "value", {
      get: function(){
        return ((this._settings.valuehash)?"#":"")+this._tags.join(this._settings.delimiter+((this._settings.valuehash)?"#":""));
      },
      set: function(str){
        this._tags = str.split(this._settings.delimiter);
        this.refresh("tags");
      }
    });

    this.defMethod("addTag", function(newTag){
      if(newTag instanceof Array){
        for(var i=0; i<newTag.legnth; i++){
          this.addTag(newTag[i]);
        }
        return;
      }
      if(
        (
          !this._settings.limit ||
          this._tags.length < this._settings.limit
        ) &&
        (
          this._settings.duplicates ||
          !this.hasTag(newTag)
        )
      ){
        if(this._settings.autohash && newTag[0] == "#"){
          newTag = newTag.substring(1);
        }
        this._tags.push(newTag);
        this.refresh("tags");
      }
    });
    this.defMethod("removeTag", function(tag){
      var index = this._tags.indexOf(tag);
      if(index > -1){
        this._tags.splice(index, 1);
      }
      this.refresh("tags");
    });
    this.defMethod("removeTagByIndex", function(index){
      this._tags.splice(index, 1);
      this.refresh("tags");
    });
    this.defMethod("removeLast", function(){
      if(this._tags.length > 0){
        this._tags.splice(this._tags.length - 1, 1);
        this.refresh("tags");
      }
    });
    this.defMethod("hasTag", function(tag){
      for(var i=0; i<this._tags.length; i++){
        if(
          (
            this._settings.casesensitive &&
            this._tags[i] == tag
          ) ||
          (
            !this._settings.casesensitive &&
            this._tags[i].toLowerCase() == tag.toLowerCase()
          )
        ){
          return true;
        }
      }
      return false;
    });

    this.renderer = function(){
      var self = this;
      var $tags = $("<div class='addui-Tags "+this._settings.class+"'></div>");
      for(var i=0; i<this._tags.length; i++){
        (function(){
          var index = i;
          $("<div class='addui-Tags-tag"+((self._settings.removable)?" addui-Tags-removable":"")+"'>"+((self._settings.autohash)?"#":"")+self._tags[i]+"</div>").on("click", function(){
            if(self._settings.removable){
              self.removeTagByIndex(index);
            }
          }).appendTo($tags);
        })();
      }
      var $input = $("<input type='text' class='addui-Tags-input'  placeholder='"+this._settings.placeholder+"'></div>").appendTo($tags);
      var $value = $("<input type='hidden' class='addui-Tags-value' id='"+this._settings.id+"' name='"+this._settings.name+"' value='"+this.value+"' />").appendTo($tags);
      $input.on("keydown", function(e){
        if(
          (self._settings.tab && e.keyCode == 9) ||
          (self._settings.space && e.keyCode == 32) ||
          (self._settings.comma && e.keyCode == 188) ||
          (self._settings.enter && e.keyCode == 13)
        ){
          self.addTag($input.val());
          $input.val("");
          e.preventDefault();
          setTimeout(function(){
            $input[0].focus();
          }, 16);
          return false;
        } else if(
          self._settings.removable &&
          self._settings.backspace &&
          e.keyCode == 8 && $input.val() == ""
        ){
          self.removeLast();
          e.preventDefault();
          setTimeout(function(){
            $input[0].focus();
          }, 16);
          return false;
        }
      });
      return $tags;
    };
    this.refresher = function($element, changed){
      var self = this;
      if(changed == "tags"){
        $element.find(".addui-Tags-tag").remove();
        for(var i=this._tags.length-1; i>-1; i--){
          (function(){
            var index = i;
            var $tag = $("<div class='addui-Tags-tag"+((self._settings.removable)?" addui-Tags-removable":"")+"'>"+((self._settings.autohash)?"#":"")+self._tags[i]+"</div>").on("click", function(){
              if(self._settings.removable){
                self.removeTagByIndex(index);
              }
            }).prependTo($element);
          })();
        }
        $element.find(".addui-Tags-value").val(this.value);
      } else return this.renderer();
    };

    this.defMethod("init", function(tags, settings){
      this.settings = settings;
      if(tags){
        if(typeof(tags) == "string"){
          tags = tags.split(this._settings.delimiter);
        }
        this.tags = tags;
      }
    });
    this.init.apply(this, arguments);
  });
  $.fn.addTags = function(settings){
    $add.Tags(this, settings);
  };
  $add.auto.Tags = function(){
    if(!$add.auto.disabled){
      $("input[data-addui=tags]").addTags();
    }
  }
})(jQuery);
$(function(){for(var k in $add.auto){if(typeof($add.auto[k])=="function"){$add.auto[k]();}}});