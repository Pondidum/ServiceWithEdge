require 'bundler/setup'
require 'albacore'

project_name = 'ServiceWithEdge'

nugets_restore :restore do |n|
	n.exe = 'tools/nuget/nuget.exe'
	n.out = 'packages'
end

build :compile do |msb|
	msb.target = [ :clean, :rebuild ]
	msb.sln = "#{project_name}.sln"
end

task :npm do |t|

	Dir.chdir "#{project_name}/bin/debug/webui" do
		system 'npm', 'install'
	end

end

task :default => [ :restore, :compile, :npm ]
