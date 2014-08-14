require 'bundler/setup'
require 'albacore'

tool_nuget = 'tools/nuget/nuget.exe'
project_name = 'Dash'
project_output = 'build'

nugets_restore :restore do |n|
	n.exe = tool_nuget
	n.out = 'packages'
end

build :compile do |msb|
	msb.target = [ :clean, :rebuild ]
	msb.sln = "#{project_name}.sln"
end

nugets_pack :pack do |n|

	Dir.mkdir(project_output) unless Dir.exists?(project_output)

	n.exe = tool_nuget
	n.out = project_output

	n.files = FileList["#{project_name}/*.csproj"]

	n.with_metadata do |m|
		m.description = 'Model based web dashboard'
		m.authors = 'Andy Dote'
		m.version = '1.0.0.0'
	end

end

# task :npm do |t|

# 	Dir.chdir "#{project_name}/bin/debug/webui" do
# 		system 'npm', 'install'
# 	end

# end

task :default => [ :restore, :compile, :pack]
